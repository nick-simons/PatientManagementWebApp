const firstOccuranceRegExp = char => new RegExp(char + '(.+)');

export const splitAtNewLine = input => input.split('\n');

export const splitAtFirstOccurance = (input, splitChar) => 
  input.split(firstOccuranceRegExp(splitChar));