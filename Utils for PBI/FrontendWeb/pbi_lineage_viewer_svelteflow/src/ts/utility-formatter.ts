export function formatDAX(daxString: string){
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
    }
    let formattedString: string = formattedStringArr.join('');
    return formattedString;
}

(window as any).formatDAX = formatDAX;
