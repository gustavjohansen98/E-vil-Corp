using System.Collections;
using System.Collections.Generic;

namespace minitwit
{
    public interface IMiniMain
    {
        string User { get; set; }
        IEnumerable<string> Messages { get; set; }

        string Url_for(string name);
    }
}