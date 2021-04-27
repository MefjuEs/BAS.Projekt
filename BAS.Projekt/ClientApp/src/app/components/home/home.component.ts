import { Component, OnInit } from '@angular/core';
import { IMovieFilters } from 'src/app/interfaces/movies/IMovieFilters';
import { IMovieListWithFilters } from 'src/app/interfaces/movies/IMovieListWithFilters';
import { MoviesService } from 'src/app/services/movies.service';
import { IFile } from 'src/app/interfaces/movies//IFile'

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  public isLoading: boolean;
  public isLoadingMoviePage: boolean;
  public movies: IMovieListWithFilters = {
    currentPage: 1,
    pageSize: 9,
    allPages: 1,
    allElements: 0,
    movieList: []
  };
  public movieFilters: IMovieFilters = {
    title: '',
    releaseYearFrom: null,
    releaseYearTo: null,
    movieLengthFrom: null,
    movieLengthTo: null,
    avgRatingFrom: null,
    avgRatingTo: null,
    page: 0,
    pageSize: 9,
    orderBy: '',
    genreId: null
  }

  constructor(private moviesService: MoviesService) {
    this.isLoading = true;
    this.isLoadingMoviePage = false;
  }

  ngOnInit() {
    this.getMovies();
  }

  getMovies() {
    this.movieFilters.page += 1;

    this.moviesService.getMovies(this.movieFilters).subscribe(data => {
      data.movieList.forEach(movie => {
        this.movies.movieList.push(movie);
      });
      this.movies.allPages = data.allPages;
      this.isLoading = false;
      this.isLoadingMoviePage = false;
    });
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
    let pageSize = this.movieFilters.pageSize;
    this.movieFilters = event;
    this.movieFilters.pageSize = pageSize;
    this.moviesService.getMovies(this.movieFilters).subscribe(data => this.movies = data);
  }

  onWindowScroll(event) {
    let pos = (document.documentElement.scrollTop || document.body.scrollTop) + document.documentElement.offsetHeight;
    let max = document.documentElement.scrollHeight;

    if(pos == max && this.movies.allPages > this.movieFilters.page) {
      this.isLoadingMoviePage = true;
      this.getMovies();
    }
  }
}
