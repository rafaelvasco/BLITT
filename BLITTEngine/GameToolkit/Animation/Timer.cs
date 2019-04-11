using System;
using System.Collections.Generic;

namespace BLITTEngine.GameToolkit.Animation
{
    public class Timer
    {
        private const int INITIAL_BUFFER_SIZE = 10;
        
        private readonly Stack<TimerTask> free_tasks_stack;
        private readonly TimerTask[] tasks_list;

        private readonly Dictionary<int, int> tasks_map;
        private int active_tasks;

        public Timer()
        {
            tasks_map = new Dictionary<int, int>(INITIAL_BUFFER_SIZE);
            tasks_list = new TimerTask[INITIAL_BUFFER_SIZE];
            free_tasks_stack = new Stack<TimerTask>(INITIAL_BUFFER_SIZE);

            for (var i = 0; i < INITIAL_BUFFER_SIZE; ++i) free_tasks_stack.Push(new TimerTask());
        }

        private TimerTask RequestTask()
        {
            if (free_tasks_stack.Count == 0) free_tasks_stack.Push(new TimerTask());

            return free_tasks_stack.Pop();
        }

        private void RegisterTask(TimerTask task)
        {
            active_tasks++;

            for (var i = 0; i < tasks_list.Length; ++i)
                if (tasks_list[i] == null)
                {
                    tasks_list[i] = task;
                    tasks_map[task.Tag] = i;
                    task.Index = i;
                    break;
                }
        }

        private void UnregisterTask(TimerTask task)
        {
            if (task.Tag == 0) return;

            active_tasks--;

            tasks_list[task.Index] = null;
            tasks_map.Remove(task.Tag);
            free_tasks_stack.Push(task);

            task.Action = null;
            task.DelayTicks = 0;
            task.Tag = 0;
            task.Index = -1;
            task.TotalTicks = 0;
            task.Repeat = 0;
            task.Counter = 0;
            task.AfterAction = null;
        }

        public void Update()
        {
            if (active_tasks == 0) return;

            for (var i = 0; i < tasks_list.Length; ++i)
            {
                var task = tasks_list[i];

                if (task == null) continue;

                task.TotalTicks += 1;

                switch (task.Type)
                {
                    case TimerTaskType.Every:
                    {
                        if (task.TotalTicks >= task.DelayTicks)
                        {
                            task.Action();
                            task.TotalTicks -= task.DelayTicks;

                            if (task.Repeat > 0)
                            {
                                task.Counter += 1;

                                if (task.Counter >= task.Repeat)
                                {
                                    task.AfterAction();
                                    UnregisterTask(task);
                                }
                            }
                        }

                        break;
                    }
                    case TimerTaskType.After:
                    {
                        if (task.TotalTicks >= task.DelayTicks)
                        {
                            task.Action();
                            UnregisterTask(task);
                        }

                        break;
                    }
                    case TimerTaskType.During:
                    {
                        task.Action();

                        if (task.TotalTicks >= task.DelayTicks)
                        {
                            task.AfterAction();
                            UnregisterTask(task);
                        }

                        break;
                    }
                    case TimerTaskType.Tween:
                        break;
                }
            }
        }

        /// <summary>
        ///     Executes Action after given delay
        /// </summary>
        /// <param name="delayTicks"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public int After(int delayTicks, Action action, string tag = null)
        {
            var _tag = tag?.GetHashCode() ?? Guid.NewGuid().GetHashCode();

            if (tag != null) Cancel(_tag);

            var task = RequestTask();

            task.Type = TimerTaskType.After;
            task.DelayTicks = delayTicks;
            task.Action = action ?? delegate { };
            task.Tag = _tag;

            RegisterTask(task);

            return _tag;
        }

        /// <summary>
        ///     Executes Action repeatedly at given interval.
        ///     It receives a number and a function, representing the interval duration
        ///     and the Action respectively. A third 'count' argument can be passed in
        ///     to limit the amount of times the Action will run.
        ///     A fourth 'after' Action can be passed to be executed at the end, if 'count' is passed.
        /// </summary>
        /// <param name="delayTicks"></param>
        /// <param name="action"></param>
        /// <param name="count"></param>
        /// <param name="after"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int Every(int delayTicks, Action action, int count = 0, Action after = null, string tag = null)
        {
            var _tag = tag?.GetHashCode() ?? Guid.NewGuid().GetHashCode();

            if (tag != null) Cancel(_tag);

            var task = RequestTask();

            task.Type = TimerTaskType.Every;
            task.DelayTicks = delayTicks;
            task.Action = action ?? delegate { };
            task.Repeat = count;
            task.AfterAction = after ?? delegate { };
            task.Tag = _tag;

            RegisterTask(task);

            return _tag;
        }

        /// <summary>
        ///     The during function will execute an action every frame for a given amount of time. It receives
        ///     a number and a function, representing the duration and Action respectively. Additionally a third
        ///     'after' Action can be passed to be executed after the specified duration.
        /// </summary>
        /// <returns></returns>
        public int During(int delayTicks, Action action, Action after = null, string tag = null)
        {
            var _tag = tag?.GetHashCode() ?? Guid.NewGuid().GetHashCode();

            if (tag != null) Cancel(_tag);

            var task = RequestTask();

            task.Type = TimerTaskType.During;
            task.DelayTicks = delayTicks;
            task.Action = action ?? delegate { };
            task.AfterAction = after ?? delegate { };
            task.Tag = _tag;

            RegisterTask(task);

            return _tag;
        }


        public void Cancel(int tag)
        {
            if (tasks_map.TryGetValue(tag, out var index))
            {
                var task = tasks_list[index];

                UnregisterTask(task);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < tasks_list.Length; ++i)
            {
                var task = tasks_list[i];
                if (task != null) UnregisterTask(task);
            }
        }

        private enum TimerTaskType
        {
            After,
            Every,
            During,
            Tween
        }

        private class TimerTask
        {
            public Action Action;
            public Action AfterAction;
            public int Counter;
            public int DelayTicks;
            public int Index;
            public int Repeat;
            public int Tag;
            public int TotalTicks;
            public TimerTaskType Type;
        }
    }
}