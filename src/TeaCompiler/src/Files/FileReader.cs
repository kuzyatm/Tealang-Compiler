using System.Text;

public class FileReader : IDisposable
{
    FileStream fs;
    int read = 0;
    byte[] buf;

    public FileReader(FileStream fs)
    {
        this.fs = fs;
        this.buf = new byte[16];
    }

    public (char[], int)? GetData()
    {
        read = fs.Read(buf);
        
        (char[], int)? ret = null;
        if (read != 0)
        {
            var c = Encoding.UTF8.GetChars(buf, 0, read);
            ret = (c, c.Length);
        }

        return ret;
    }

    public void Dispose()
    {
        fs.Close();
    }
}
