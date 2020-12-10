# Challenge Instructions
1. Get a Farhrenheit temperature value between -40 and 130 as input from the user.
    - Use int.tryParse(string, out parsedValue) to parse the 
    input. (make sure to instantiate an int to capture the
    out parameter.) int.tryParse() returns true if successful
    - Use 'if'/'else if' statements or a switch statememt to give instructions
    on what to wear for that temperature.

2. Create a user Login System, where the user can first register and then Login in. 
    - The Program should check if the user has entered the correct username and password, when logging in (so the same ones that he used when registering).
    - As we haven't covered storing data yet, just create the program in a way, that registering and logging in, happen in the same execution of it.
    - Use If statements and User Input and Methods to solve the challenge.

3. Get a Farhrenheit temperature value between -40 and 130 as input from the user.
    - Validate that the input is a valid integer value. 
    - If the input value is not a valid integer value, it should print to the console "Not a valid temperature" and repeat the promptfor a valid temperature.
    - If the input temperature value is <=42, print "[tempValue] is too cold!" to the console.
    - If the input temperature value is >= 43 and <=78 print "[tempValue] is an ok temperature" to the console.
    - If the input temperature value is > 78 it should write "[tempValue] is too hot!" to the console.
    - You must use ternary operators (not 'if' statements) to check for the three conditions, 
    however you can use an if statement for the other conditions like to check if the 
    entered value is a valid integer or not. You may use previous methods (hint hint) 
    to get or varify values.

4. Create an activity advice method that takes in an int value for the temperature and suggests an outdoor activity as well as give its opinion on the weather. The opinion should be printed on the console.
    - n < -20 = hella cold
    - -20 <= n < 0 = pretty cold
    - 0 <= n < 20 = cold
    - 20 <= n < 40 = thawed out
    - 40 <= n < 60 = feels like Autumn
    - 60 <= n < 80 = perfect outdoor workout temperature
    - 80 <= n < 90 = niiice
    - 90 <= n < 100 = hella hot
    - 100 <= n < 135 = hottest

