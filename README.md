# VPKSoft.UNCUtil
An utility to access Windows ([SMB](https://en.wikipedia.org/wiki/Server_Message_Block)/[CIFS](https://en.wikipedia.org/wiki/Server_Message_Block)) shares with credentials.

## Usage
```C#
try // an error might occur..
{
  // IDisposable so using..
  using (UNCLogin share1 = new UNCLogin(@"\\server1\share1",
    new System.Net.NetworkCredential("username1", "password1", "domain1")))
  {
    // IDisposable so using..
    using (UNCLogin share2 =
      new UNCLogin(@"\\server2\share2", new System.Net.NetworkCredential("username2", "password2", "domain2")))
    {
      // a normal file operation with the given credentials.
      File.Copy(@"\\server1\share1\somefile.dat", @"\\server2\share2\somefile.dat");
    }
  }
}
catch (Exception ex)
{
  // show the error message..
  MessageBox.Show(ex.Message);
}
```
