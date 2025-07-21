export function formatDAX(daxString: string){
    //TO-DO: $SYSTEM.DISCOVER_KEYWORDS - Format Keywords by getting the list of keywords from this table
    const bracket_stack: string[] = [];
    let indentation = "    ", indentationCount = 0;
    let formattedStringArr: string[] = [];
    for (let i=0; i < daxString.length; i++){
        let currentChar = daxString[i];
        formattedStringArr.push(currentChar);

        if(['('].includes(currentChar)){
            bracket_stack.push(currentChar);
            formattedStringArr.push('\n');
            indentationCount++;
            formattedStringArr.push(indentation.repeat(indentationCount));
        }
        else if ([')'].includes(currentChar)){
            bracket_stack.pop();
            formattedStringArr.push('\n');
            indentationCount--;
            formattedStringArr.push(indentation.repeat(indentationCount));
        }
        // TO-DO: Need to enhance to exclude commas in "," or commas succeeding a bracket
        // else if ([','].includes(currentChar)){
        //     formattedStringArr.push('\n');
        //     formattedStringArr.push(indentation.repeat(indentationCount));
        // }
    }
    let formattedString: string = formattedStringArr.join('');
    return formattedString;
}

//TO-DO: Code can be removed in final version. Here to debug the function through browser
(window as any).formatDAX = formatDAX;
