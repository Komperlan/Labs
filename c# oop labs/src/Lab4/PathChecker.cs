namespace Itmo.ObjectOrientedProgramming.Lab4;

public class PathChecker
{
    public bool CheckIsAbsolutePath(string path)
    {
        if (path.Length < 3)
        {
            return false;
        }

        if (path[0] < 'A' || path[0] > 'Z')
        {
            return false;
        }

        if (path[1] != ':' && path[2] != '/')
        {
            return false;
        }

        return true;
    }
}
