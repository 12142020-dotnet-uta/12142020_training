function BasicFunction()
{
    alert("Hello World of JS!");
}
function ParentFunction(callBack)
{
    console.log("In parent function");
    let name = prompt('Please enter your name');
    callBack(name);
    console.log("Back in parent function");
}
function CallBack(name)
{
    console.log("Hello" + name);
    console.log("calling back");
}
function CallBack2()
{
    console.log("In other function, calling back");
}
//This IIFE uses the outer function's count parameter
let Outer = (() =>
{
    let count=0;
    return function inner()
    {
        return count+=1;
    };
})();

let addMore = (() => {
    let count = 0;
    return () => {
        count +=1;
        return count;
    };
});
let add = addMore();
let addagain = addMore();