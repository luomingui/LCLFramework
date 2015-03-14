using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace LCL.MetaModel
{
    /// <summary>
    /// 所有命令的基类
    /// </summary>
    public abstract class Command : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// 是否这个命令所对应的按钮可以被执行
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool CanExecute(object param)
        {
            try
            {
                return this.CanExecuteCore(param);
            }
            catch
            {
                return false;
            }
        }
        protected virtual bool CanExecuteCore(object param) { return true; }
        /// <summary>
        /// 执行这个命令
        /// </summary>
        /// <param name="param"></param>
        public void Execute(object param)
        {
            try
            {
                this.ExecuteCore(param);
                this.OnExecuted();
            }
            catch (Exception ex)
            {
                var args = new CommandExecuteFailedArgs(ex, param);
                this.OnExecuteFailed(args);
                if (!args.Cancel) throw;
            }
        }
        protected abstract void ExecuteCore(object param);

        #region 事件

        /// <summary>
        /// 执行成功后的事件。
        /// </summary>
        public event EventHandler Executed;
        protected virtual void OnExecuted()
        {
            var handler = this.Executed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        /// <summary>
        /// 执行发生异常后的事件。
        /// </summary>
        public event EventHandler<CommandExecuteFailedArgs> ExecuteFailed;

        protected virtual void OnExecuteFailed(CommandExecuteFailedArgs e)
        {
            var handler = this.ExecuteFailed;
            if (handler != null) handler(this, e);
        }

        #endregion

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}