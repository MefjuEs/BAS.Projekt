import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IMovieFilters } from 'src/app/interfaces/movies/IMovieFilters';
import { IMovieInList } from 'src/app/interfaces/movies/IMovieInList';
import { IMovieListWithFilters } from 'src/app/interfaces/movies/IMovieListWithFilters';
import { MoviesService } from 'src/app/services/movies.service';

@Component({
  selector: 'admin-movie',
  templateUrl: './admin-movie.component.html',
  styleUrls: ['./admin-movie.component.css']
})
export class AdminMovieComponent implements OnInit {
  public isLoading: boolean = true;
  public movieFilters: IMovieFilters = {
    title: '',
    releaseYearFrom: null,
    releaseYearTo: null,
    movieLengthFrom: null,
    movieLengthTo: null,
    avgRatingFrom: null,
    avgRatingTo: null,
    page: 1,
    pageSize: 10,
    orderBy: '',
    genreId: null
  }
  public movies: IMovieListWithFilters = {
    currentPage: 1,
    pageSize: 10,
    allPages: 1,
    allElements: 0,
    movieList: []
  };
  public displayedColumns: string[] = ['title', 'releaseYear', 'movieLengthInMinutes', 'symbol'];
  public pageIndex: number;
  
  constructor(private moviesService: MoviesService, public dialog: MatDialog) {
    this.isLoading = true;
  }

  ngOnInit() {
    this.pageIndex = 0;
    this.movieFilters.pageSize = 10;
    this.getMovies();
  }

  getMovies() {
    this.moviesService.getMovies(this.movieFilters).subscribe(data => {
      this.isLoading = true;
      this.movies = data;
      this.isLoading = false;
    });
  }

  onApplyFilters(event) {
    let pageSize = this.movieFilters.pageSize;
    this.movieFilters = event;
    this.movieFilters.pageSize = pageSize;
    this.movieFilters.page = 1;
    this.pageIndex = 0;
    this.getMovies();
  }

  changePage(event) {
    if (event.pageSize != this.movieFilters.pageSize) {
      this.movieFilters.pageSize = event.pageSize;
    }
    this.pageIndex = event.pageIndex;

    this.movieFilters.page = this.pageIndex + 1;
    this.getMovies();

    console.log(event)
    console.log(this.movieFilters.page)
  }

  openDeleteDialog(id) {
    const dialogRef = this.dialog.open(DeleteMovieDialog);

    dialogRef.afterClosed().subscribe(result => {
      if(result === true) {
        this.moviesService.deleteMovie(id).subscribe(() => {
          this.getMovies();
          console.log("Pomyślnie usunięto");
        })
      }
    });
  }
}

@Component({
  selector: 'delete-movie-dialog',
  templateUrl: './delete-movie-dialog.html',
})
export class DeleteMovieDialog {}
