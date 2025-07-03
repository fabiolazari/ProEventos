import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/constants';

@Pipe({
  name: 'DateFormatPipe'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any): any {
    if (!value) return null;
    const [day, month, year, time] = value.match(/(\d{2})\/(\d{2})\/(\d{4}) (\d{2}:\d{2}:\d{2})/)!.slice(1);
    const isoDate = new Date(`${year}-${month}-${day}T${time}`);

    return super.transform(isoDate, Constants.DATE_TIME_FMT) ?? 'data inválida...';
  }
}
