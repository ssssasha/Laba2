using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    public interface IStrategy
    {
        List<Films> AnalyzeFile(Films mySearch, string path);
    }
}
