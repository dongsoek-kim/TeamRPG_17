namespace TeamRPG_17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                GameManager.Instance.SceneUpdate();
            }
        }
    }
}
