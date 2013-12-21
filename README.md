[Xbox.Music](http://github.com/AdvancedREI/Xbox.Music)
=================

Xbox.Music is a Portable Class Library that makes it easy to interact with the new Xbox Music API.

Works on .NET 4.5, Windows Phone 8, and Windows 8.x, as well as Android and iOS through Mono.

Quick start
-----------

Install the NuGet package: `Install-Package Xbox.Music -Pre`, clone the repo, `git clone git://github.com/advancedrei/Xbox.Music.git`, or [download the latest release](https://github.com/advancedrei/Xbox.Music/zipball/master).


Quick start
-----------
Here is a simple example of leveraging the MusicClient to query an Artist.

```csharp
// Take advantage of built-in Point of Interest groups
var client = new MusicClient("YOUR CLIENT ID", "YOUR CLIENT SECRET");

// The MusicClient handles OAuth authentication internally, no need to worry about the methodology in the official docs.
var results = await client.Find("Daft Punk")

Debug.WriteLine(results.Artists.Items.Count);
```

For more information, check out our [online documentation at NuDoq](http://www.nudoq.org/#!/Projects/Xbox.Music).

Bug tracker
-----------

Have a bug? Please create an issue here on GitHub that conforms with [necolas's guidelines](https://github.com/necolas/issue-guidelines).

https://github.com/AdvancedREI/Xbox.Music/issues



Twitter account
---------------

Keep up to date on announcements and more by following AdvancedREI on Twitter, [@AdvancedREI](http://twitter.com/AdvancedREI).



Blog
----

Read more detailed announcements, discussions, and more on [The AdvancedREI Dev Blog](http://advancedrei.com/blogs/development).


Authors
-------

**Robert McLaws**

+ http://twitter.com/robertmclaws
+ http://github.com/advancedrei


Copyright and license
---------------------

Copyright 2013 AdvancedREI, LLC.

The MIT License (MIT)

Copyright (c) 2013 AdvancedREI, LLC. and Robert McLaws

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

- The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

- You may not use this software to sell apps in the [Windows Phone Store](http://www.windowsphone.com/en-US/store/publishers?publisherId=AdvancedREI%252c%2BLLC.&appId=42268b66-a8ed-46ea-9355-1287522a7cf9) or Windows Store that replicate functionality from apps distributed by AdvancedREI.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.