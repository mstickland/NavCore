using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavConsole {

    public class ConsoleProgress<T> : IProgress<T> {

        private Action<T> _reportAction;

        public ConsoleProgress(Action<T> reportAction) {
            if(reportAction == null)
                throw new ArgumentException();
            _reportAction = reportAction;
        }

        public void Report(T value) {
            _reportAction(value);
        }
    }
}
