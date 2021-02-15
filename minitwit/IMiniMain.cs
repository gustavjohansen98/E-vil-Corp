namespace minitwit
{
    public interface IMiniMain
    {
        string User { get; set; }

        string Url_for(string name);
    }
}