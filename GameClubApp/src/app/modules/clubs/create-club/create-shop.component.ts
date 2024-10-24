import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { GameClubService } from '../../../services/game-club.service';
import { AlertService } from '../../../services/alert.service';

@Component({
    selector: 'app-create-club',
    templateUrl: './create-club.component.html',
    styleUrls: ['./create-club.component.css']
})

export class CreateClubComponent implements OnInit {
    form!: FormGroup;

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor(private fb: FormBuilder,
        private gameClubService: GameClubService,
        private alertService: AlertService,
        private router: Router,
    ) {
    };

    ngOnInit(): void {
        this.form = this.fb.group({
            name: [null, Validators.required],
            description: [null]
        });
    };

    save() {
        let data = {
            Name: this.form.get("name")?.value,
            Description: this.form.get("description")?.value
        };

        this.gameClubService.addClub(data)
            .subscribe(() => {
                this.alertService.showToastSuccess();
                this.router.navigateByUrl("/clubs");
            }, (err) => {
                this.alertService.showToastError(err);
                console.log(err);
            });
    }

}
