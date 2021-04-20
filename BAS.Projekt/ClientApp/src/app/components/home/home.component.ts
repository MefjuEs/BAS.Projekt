import { Component, OnInit } from '@angular/core';
import { IMovieFilters } from 'src/app/interfaces/movies/IMovieFilters';
import { IMovieListWithFilters } from 'src/app/interfaces/movies/IMovieListWithFilters';
import { MoviesService } from 'src/app/services/movies.service';
import { IFile } from 'src/app/interfaces/movies//IFile'
import { GenresService } from 'src/app/services/genres.service';
import { IGenreList } from 'src/app/interfaces/genres/IGenreList';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  public movies: IMovieListWithFilters;
  public genresList: IGenreList;
  public movieFilters: IMovieFilters = {
    title: '',
    releaseYearFrom: null,
    releaseYearTo: null,
    movieLengthFrom: null,
    movieLengthTo: null,
    avgRatingFrom: null,
    avgRatingTo: null,
    page: 1,
    pageSize: null,
    orderBy: '',
    genreId: null
  }

  constructor(private moviesService: MoviesService, private genresService: GenresService) { }

  ngOnInit() {
    this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
    this.genresService.getGenres().subscribe(data => this.genresList = data);
  }

  getMoviePoster(poster: IFile) {
    if(poster != null) {
      return `data:${poster.contentType};base64,${poster.file}`;
    }
    else {
      return '';
    }
  }

  onApplyFilters(event) {
    this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
  }
}
