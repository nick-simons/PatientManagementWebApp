export const sortArrayByKeyValue = (array, key, convertKeyToNumber) => {
    return array.sort((a, b) => {
      if(convertKeyToNumber) {
        return +a[key] > +b[key] ? 1 : +a[key] < +b[key] ? -1 : 0;
      }
      return a[key] > b[key] ? 1 : a[key] < b[key] ? -1 : 0;
    });
  };
  
  export default {};