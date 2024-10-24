import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../../guards/auth.guard";
import { MaterialModule } from "../../material.module";
import { SharedModule } from "../../shared.module";
import { ClubListComponent } from "./club-list/club-list.component";
import { ClubsComponent } from "./clubs.component";
import { CreateClubComponent } from "./create-club/create-shop.component";
import { ClubDetailComponent } from "./club-detail/club-detail.component";
import { CreateEventComponent } from "./create-event/create-event.component";

const routes: Routes = [
  {
    path: "clubs",
    component: ClubsComponent,
    children: [
      {
        path: "",
        component: ClubListComponent,
        canActivate: [AuthGuard],
      },
      {
        path: "create-club",
        component: CreateClubComponent,
        canActivate: [AuthGuard],
      },
      // {
      //   path: "clubs/:id/create-event",
      //   component: CreateClubEventComponent,
      //   canActivate: [AuthGuard],
      // },
      {
        path: ":id",
        component: ClubDetailComponent,
        canActivate: [AuthGuard],
      }

    ],
  },
];

@NgModule({
  declarations: [
    ClubsComponent,
    ClubListComponent,
    CreateClubComponent,
    ClubDetailComponent,
    CreateEventComponent,
  ],
  imports: [
    SharedModule,
    MaterialModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    RouterModule.forChild(routes),
  ],
  providers: [AuthGuard],
  bootstrap: [ClubsComponent],
})
export class ClubsModule {}
