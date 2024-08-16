namespace RandomPicFind.Classes;

public class Rootobject
{
    public Result[] results { get; set; }
    public string next { get; set; }
}

public class Result
{
    public string id { get; set; }
    public string title { get; set; }
    public string content_description { get; set; }
    public string content_rating { get; set; }
    public string h1_title { get; set; }
    public Medium[] media { get; set; }
    public string bg_color { get; set; }
    public float created { get; set; }
    public string itemurl { get; set; }
    public string url { get; set; }
    public object[] tags { get; set; }
    public object[] flags { get; set; }
    public int shares { get; set; }
    public bool hasaudio { get; set; }
    public bool hascaption { get; set; }
    public string source_id { get; set; }
    public object composite { get; set; }
}

public class Medium
{
    public Nanomp4 nanomp4 { get; set; }
    public Mediumgif mediumgif { get; set; }
    public Tinywebm tinywebm { get; set; }
    public Nanowebm nanowebm { get; set; }
    public Loopedmp4 loopedmp4 { get; set; }
    public Nanogif nanogif { get; set; }
    public Gif gif { get; set; }
    public Mp4 mp4 { get; set; }
    public Webm webm { get; set; }
    public Tinygif tinygif { get; set; }
    public Tinymp4 tinymp4 { get; set; }
}

public class Nanomp4
{
    public float duration { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
    public string preview { get; set; }
    public int size { get; set; }
}

public class Mediumgif
{
    public string preview { get; set; }
    public string url { get; set; }
    public int[] dims { get; set; }
    public int size { get; set; }
}

public class Tinywebm
{
    public int size { get; set; }
    public string preview { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
}

public class Nanowebm
{
    public string url { get; set; }
    public int size { get; set; }
    public int[] dims { get; set; }
    public string preview { get; set; }
}

public class Loopedmp4
{
    public string preview { get; set; }
    public float duration { get; set; }
    public string url { get; set; }
    public int[] dims { get; set; }
    public int size { get; set; }
}

public class Nanogif
{
    public string preview { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
    public int size { get; set; }
}

public class Gif
{
    public int size { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
    public string preview { get; set; }
}

public class Mp4
{
    public int[] dims { get; set; }
    public int size { get; set; }
    public string preview { get; set; }
    public string url { get; set; }
    public float duration { get; set; }
}

public class Webm
{
    public int size { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
    public string preview { get; set; }
}

public class Tinygif
{
    public int size { get; set; }
    public int[] dims { get; set; }
    public string url { get; set; }
    public string preview { get; set; }
}

public class Tinymp4
{
    public string url { get; set; }
    public float duration { get; set; }
    public int size { get; set; }
    public string preview { get; set; }
    public int[] dims { get; set; }
}