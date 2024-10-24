import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GameClubService } from '../../../services/game-club.service';
import { AlertService } from '../../../services/alert.service';
import { MatDialog } from '@angular/material/dialog';
import { CreateEventComponent } from '../create-event/create-event.component';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-club-detail',
  templateUrl: './club-detail.component.html',
})

export class ClubDetailComponent implements OnInit, AfterViewInit {
  clubId!: any;
  club!: any;
  events!: any;
  dataSource: any;

  displayedColumns: string[] = ['id', 'title', 'description', 'scheduled'];

  constructor(
    private dialog: MatDialog,
    private gameClubService: GameClubService,
    private alertService: AlertService,
    private router: Router,
    private activeRoute: ActivatedRoute,
  ) { }

  ngAfterViewInit(): void {
    this.getClubEvents();
    this.getClub();
  }

  ngOnInit(): void {
    this.clubId = this.activeRoute.snapshot.params['id'];
  }

  getClub() {
    var queryParams = {
      id: this.clubId
    };

    this.gameClubService.getClubs(queryParams)
      .subscribe(res => {
        this.club = res[0];
        console.log(this.club);
      }, (err) => {
        this.alertService.showToastError(err);
        console.log(err);
      });
  }

  getClubEvents() {
    this.gameClubService.getClubEvents(this.clubId)
      .subscribe(res => {
        this.events = res;
      }, (err) => {
        this.alertService.showToastError(err);
        console.log(err);
      });
  }

  addEvent() {
    const dialogRef = this.dialog.open(CreateEventComponent, {
      data: {clubId: this.clubId},
      width: '50%'
    });

    dialogRef.afterClosed().subscribe(() => {
      setTimeout(() => {
        this.getClubEvents();
        //this.getClub();
      }, environment.loadTimeout);
    });
  }

}
