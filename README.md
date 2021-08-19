#  WrapperCoinMP

This project objective is to create a Simple wrapper for the https://github.com/coin-or/CoinMP project. It is written
using .NET 5.0 and can be run either on windows or unix environment. If working on windows, it can be specified in a file WrapperConfig.json 
the path where the CoinMP.dll is on your computer; the config file can be put either in the folder in which the program in run (e.g. for the TestMain project
in \TestMain\bin\Debug\net5.0\) or in the folder where the code is  (e.g. for the TestMain project in \TestMain).If the config file is not present,
it is necessary to put the CoinMP.dll, if used on windows, or the libCoinMp.so in the folder where the program using this library is used.The wrapper uses 3 new functions,
not implemented in the actual release version of the CoinMP library but implemented by me in a personal version of CoinMP:
-AddRow
-NullifyRow
-AddColumn
If, using the WrapperCoinMP library with the actual release version of CoinMP, one uses one of this 3 methods there is going to be an exception. I'm actually going to ask to the owners of the project if my new version of CoinMp, with the 3 listed methods, can be usefull to the project; until then this wrapper is going to fail if the 3 functions are used; i apologize for that, you can still use all the other functionality of the wrapper.
In the WrapperTest project there is a sample of the use of the wrapper.
Additionally, there is a release with the compiled dll of the project and there will be a nuget package called WrapperCoinMP.
