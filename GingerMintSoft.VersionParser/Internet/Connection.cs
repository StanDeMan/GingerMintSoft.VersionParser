namespace GingerMintSoft.VersionParser.Internet
{
    public static class Connection
    {
        public static async Task<bool> CheckAsync()
        {
            try
            {
                using var client = new HttpClient();
                using (await client.GetAsync("http://google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
