import { splitAtFirstOccurance, splitAtNewLine } from './stringUtils';

export const parseCsv = input => {
  const tableData = splitAtNewLine(input);
  const output = tableData.map(row => {
    const rowData = splitAtFirstOccurance(row, ',');
    const [seriesName, rowDetails] = rowData;
    const seriesData = rowDetails.split(',').map(cell => {
      const cellData = cell.split('|');
      const [year, score] = cellData;
      return {
        name: year,
        value: +score
      };
    });

    return {
      name: seriesName,
      series: seriesData
    };
  });
  return output;
};

export default {};