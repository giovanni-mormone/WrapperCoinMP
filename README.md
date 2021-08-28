#  WrapperCoinMP

This project objective is to create a Simple wrapper for the https://github.com/coin-or/CoinMP project. It is written using \
.NET 5.0 and can be run either on windows or unix environment. If working on windows, the path where the CoinMP.dll is on your computer 
can be specified in a file WrapperConfig.json; if running on Linux, the libCoinMP.so MUST be put in the same directory where the project runs.
The WrapperConfig.json file can be put either in the folder in which the program in run \
(e.g. for the TestMain project in \TestMain\bin\Debug\net5.0\) or in the folder where the code is  (e.g. for the TestMain project in \TestMain).
If the config file is not present it is necessary to put the CoinMP.dll in the folder where the program using this library is runs.
The wrapper uses 3 new functions, not implemented in the actual release version of the CoinMP library but 
implemented by me in a personal forked version of CoinMP that can be found here https://github.com/giovanni-mormone/CoinMP: \
-AddRow\
-NullifyRow\
-AddColumn\
If, using the WrapperCoinMP library with the actual release version of CoinMP, one uses one of this 3 methods there is going to be an exception. If you need those 3 methods feel free to download the .dll or .so files found in the forked version of CoinMP -> https://github.com/giovanni-mormone/CoinMP.
In the WrapperTest project there is a sample of the use of the wrapper.
Additionally, there is a release with the compiled dll of the project and a nuget package called WrapperCoinMP.
