import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MoviesService } from './services/movies.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { GenresService } from './services/genres.service';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { MatTabsModule } from '@angular/material/tabs';
import { AdminMovieComponent, DeleteMovieDialog } from './components/admin-movie/admin-movie.component';
import { MatTableModule } from '@angular/material/table';
import { MovieFilterComponent } from './components/movie-filter/movie-filter.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AddEditMovieComponent } from './components/add-edit-movie/add-edit-movie.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ReactiveFormsModule } from '@angular/forms';
import { PersonnelService } from './services/personnel.service';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { AdminPersonnelComponent, DeletePersonnelDialog } from './components/admin-personnel/admin-personnel.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { AddEditPersonnelComponent } from './components/add-edit-personnel/add-edit-personnel.component';
let AppModule = class AppModule {
};
AppModule = __decorate([
    NgModule({
        declarations: [
            AppComponent,
            NavbarComponent,
            HomeComponent,
            AdminPanelComponent,
            AdminMovieComponent,
            MovieFilterComponent,
            AddEditMovieComponent,
            DeleteMovieDialog,
            AdminPersonnelComponent,
            DeletePersonnelDialog,
            AddEditPersonnelComponent
        ],
        imports: [
            BrowserModule,
            FormsModule,
            HttpClientModule,
            RouterModule.forRoot([
                { path: '', component: HomeComponent },
                { path: 'admin', component: AdminPanelComponent },
                { path: 'movie/add', component: AddEditMovieComponent },
                { path: 'movie/edit/:id', component: AddEditMovieComponent },
                { path: 'admin/personnel/add', component: AddEditPersonnelComponent },
                { path: 'admin/personnel/edit/:id', component: AddEditPersonnelComponent }
            ]),
            BrowserAnimationsModule,
            MatChipsModule,
            MatIconModule,
            MatExpansionModule,
            MatInputModule,
            MatFormFieldModule,
            MatButtonModule,
            MatSelectModule,
            MatCardModule,
            MatProgressSpinnerModule,
            MatTabsModule,
            MatTableModule,
            MatPaginatorModule,
            MatDialogModule,
            ReactiveFormsModule,
            MatAutocompleteModule,
            MatDatepickerModule,
            MatNativeDateModule
        ],
        providers: [
            MoviesService,
            GenresService,
            PersonnelService,
            { provide: MAT_DATE_LOCALE, useValue: 'pl-PL' }
        ],
        bootstrap: [AppComponent]
    })
], AppModule);
export { AppModule };
//# sourceMappingURL=app.module.js.map