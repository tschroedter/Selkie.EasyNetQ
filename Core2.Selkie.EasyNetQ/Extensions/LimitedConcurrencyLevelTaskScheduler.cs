using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Core2.Selkie.EasyNetQ.Extensions
{
    [ExcludeFromCodeCoverage]
    // https://msdn.microsoft.com/en-us/library/ee789351
    // Provides a task scheduler that ensures a maximum concurrency level while  
    // running on top of the thread pool. 
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        // Creates a new instance with the specified degree of parallelism.  
        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            if ( maxDegreeOfParallelism < 1 )
            {
                throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));
            }
            m_MaxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        // Indicates whether the current thread is processing work items.
        [ThreadStatic]
        private static bool s_CurrentThreadIsProcessingItems;

        // Gets the maximum concurrency level supported by this scheduler.  
        public sealed override int MaximumConcurrencyLevel => m_MaxDegreeOfParallelism;

        // The maximum concurrency level allowed by this scheduler.  
        private readonly int m_MaxDegreeOfParallelism;

        // The list of tasks to be executed  
        private readonly LinkedList <Task> m_Tasks = new LinkedList <Task>(); // protected by lock(_tasks) 

        // Indicates whether the scheduler is currently processing work items.  
        private int m_DelegatesQueuedOrRunning;

        // Gets an enumerable of the tasks currently scheduled on this scheduler.  
        protected sealed override IEnumerable <Task> GetScheduledTasks()
        {
            var lockTaken = false;
            try
            {
                Monitor.TryEnter(m_Tasks,
                                 ref lockTaken);
                if ( lockTaken )
                {
                    return m_Tasks;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            finally
            {
                if ( lockTaken )
                {
                    Monitor.Exit(m_Tasks);
                }
            }
        }

        // Queues a task to the scheduler.  
        protected sealed override void QueueTask(Task task)
        {
            // Add the task to the list of tasks to be processed.  If there aren't enough  
            // delegates currently queued or running to process tasks, schedule another.  
            lock ( m_Tasks )
            {
                m_Tasks.AddLast(task);
                if ( m_DelegatesQueuedOrRunning >= m_MaxDegreeOfParallelism )
                {
                    return;
                }
                ++m_DelegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }

        // Attempt to remove a previously scheduled task from the scheduler.  
        protected sealed override bool TryDequeue(Task task)
        {
            lock ( m_Tasks )
            {
                return m_Tasks.Remove(task);
            }
        }

        // Attempts to execute the specified task on the current thread.  
        protected sealed override bool TryExecuteTaskInline(Task task,
                                                            bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support inlining 
            if ( !s_CurrentThreadIsProcessingItems )
            {
                return false;
            }

            // If the task was previously queued, remove it from the queue 
            if ( taskWasPreviouslyQueued )
            {
                // Try to run the task.  
                return TryDequeue(task) && TryExecuteTask(task);
            }
            return TryExecuteTask(task);
        }

        // Inform the ThreadPool that there's work to be executed for this scheduler.  
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
                                               {
                                                   // Note that the current thread is now processing work items. 
                                                   // This is necessary to enable inlining of tasks into this thread.
                                                   s_CurrentThreadIsProcessingItems = true;
                                                   try
                                                   {
                                                       // Process all available items in the queue. 
                                                       while ( true )
                                                       {
                                                           Task item;
                                                           lock ( m_Tasks )
                                                           {
                                                               // When there are no more items to be processed, 
                                                               // note that we're done processing, and get out. 
                                                               if ( m_Tasks.Count == 0 )
                                                               {
                                                                   --m_DelegatesQueuedOrRunning;
                                                                   break;
                                                               }

                                                               // Get the next item from the queue
                                                               item = m_Tasks.First.Value;
                                                               m_Tasks.RemoveFirst();
                                                           }

                                                           // Execute the task we pulled out of the queue 
                                                           TryExecuteTask(item);
                                                       }
                                                   }
                                                   // We're done processing items on the current thread 
                                                   finally
                                                   {
                                                       s_CurrentThreadIsProcessingItems = false;
                                                   }
                                               },
                                               null);
        }
    }
}