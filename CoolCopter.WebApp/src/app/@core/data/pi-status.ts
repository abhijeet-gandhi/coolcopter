import { Observable } from 'rxjs';

export interface PiStatus {
  date: string;
  name: string;
  id: string;
}

export abstract class PiStatusData {
  abstract getPiStatusData(): Observable<PiStatus[]>;
}
