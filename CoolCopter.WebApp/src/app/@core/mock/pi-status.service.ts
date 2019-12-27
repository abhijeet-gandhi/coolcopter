import { Injectable } from '@angular/core';
import { of as observableOf,  Observable } from 'rxjs';
import { PiStatus, PiStatusData } from '../data/pi-status';

@Injectable()
export class PiStatusService extends PiStatusData {

  data = {};

  constructor() {
    super();
    this.data = {
    };
  }

  getPiStatusData(): Observable<PiStatus[]> {
    return observableOf();
  }
}
