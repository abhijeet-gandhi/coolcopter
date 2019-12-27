import { Component, OnDestroy } from '@angular/core';
import { NbThemeService } from '@nebular/theme';
import { takeWhile } from 'rxjs/operators';

import { PiStatusData, PiStatus } from '../../../@core/data/pi-status';

@Component({
  selector: 'ngx-pi-status',
  styleUrls: ['./pi-status.component.scss'],
  templateUrl: './pi-status.component.html',
})
export class PiStatusomponent implements OnDestroy {

  private alive = true;

  piStatus: PiStatus[] = [];
  type = 'month';
  types = ['week', 'month', 'year'];
  currentTheme: string;

  constructor(private themeService: NbThemeService,
              private userActivityService: PiStatusData) {
    this.themeService.getJsTheme()
      .pipe(takeWhile(() => this.alive))
      .subscribe(theme => {
        this.currentTheme = theme.name;
    });

    this.getPiStatus();
  }

  getPiStatus() {
    this.userActivityService.getPiStatusData()
      .pipe(takeWhile(() => this.alive))
      .subscribe(piStatusData => {
        this.piStatus = piStatusData;
      });
  }

  ngOnDestroy() {
    this.alive = false;
  }
}
