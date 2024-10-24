import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GameClubService } from '../../../services/game-club.service';
import { AlertService } from '../../../services/alert.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
})

export class CreateEventComponent implements OnInit {
  form!: FormGroup;
  clubId!: any;

  constructor(
    private fb: FormBuilder,
    private alertService: AlertService,
    private gameClubService: GameClubService,
    private activeRoute: ActivatedRoute,
    public dialogRef: MatDialogRef<CreateEventComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: [null, Validators.required],
      description: [null],
      scheduled: [null, Validators.required],
    });

    this.data = this.data;

    console.log(this.data);
  }


  okClick(): void {
    let data = {
      title: this.form.get("title")?.value,
      description: this.form.get("description")?.value,
      scheduled: this.form.get("scheduled")?.value,
    };

    this.gameClubService.addClubEvent(this.data.clubId, data)
      .subscribe(() => {
        this.alertService.showToastSuccess();
      }, (err) => {
        this.alertService.showToastError(err);
        console.log(err);
      });


    this.dialogRef.close();
  }

  close() {
    this.dialogRef.close();
  }


}
