using GPU_Prices_Parser.Data;

namespace GPU_Prices_Parser.Parsers.Files
{
    internal interface IFileParser
    {
        IData ParseFile(string path);
    }
}