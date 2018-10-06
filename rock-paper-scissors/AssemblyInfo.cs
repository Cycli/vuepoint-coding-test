// The RockPaperScissors classes aren't declared public, which means they will
// not be accessible outside the assembly. To give the test assembly privileged access,
// we need to say so here...
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("rock-paper-scissors-test")]
