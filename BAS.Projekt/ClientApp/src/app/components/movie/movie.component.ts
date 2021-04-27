import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FilmCrew } from 'src/app/interfaces/FilmCrew';
import { IFile } from 'src/app/interfaces/movies/IFile';
import { IGetMovieDTO } from 'src/app/interfaces/movies/IGetMovieDTO';
import { IPersonnelInMovieDTO } from 'src/app/interfaces/personnel/IPersonnelInMovieDTO';
import { MoviesService } from 'src/app/services/movies.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {

  public isLoading: boolean;
  public notFound: boolean;
  public movie: IGetMovieDTO;
  public actors: IPersonnelInMovieDTO[];
  public directors: IPersonnelInMovieDTO[];
  public writers: IPersonnelInMovieDTO[];

  constructor(private route: ActivatedRoute, private movieService: MoviesService) {
    this.isLoading = true;
    this.notFound = false;
    this.movie = {
      id: 0,
      title: '',
      description: '',
      releaseYear: 0,
      movieLengthInMinutes: 0,
      averageRating: 0,
      moviePoster: null,
      genres: [],
      personnel: [],
      reviews: []
    }

    this.actors = [];
    this.directors = [];
    this.writers = [];
  }

  ngOnInit() {
    this.getMovie(this.route.snapshot.params.id);
  }

  async getMovie(id) {
    try {
      this.isLoading = true;
      this.movie = await this.movieService.getMovie(id);
      this.splitMoviePersonnel(this.movie.personnel);
      this.isLoading = false;
    } 
    catch(exception) {
      this.isLoading = false;
      this.notFound = true;
    }
  }

  getMoviePoster(poster: IFile) {
    if(poster != null) {
      return `data:${poster.contentType};base64,${poster.file}`;
    }
    else {
      return '';
    }
  }
  
  getMovieLengthInHours(movieLength) {
    let hours = Math.floor(movieLength / 60);
    let minutes = movieLength % 60;

    let movieLengthInHours: string = hours + "h " + minutes + "min ";

    return movieLengthInHours;
  }

  splitMoviePersonnel(personnel) {
    personnel.forEach(person => {
      if(person.memberPosition == FilmCrew.Actor) {
        this.actors.push(person);
      } else if(person.memberPosition == FilmCrew.Director) {
        this.directors.push(person);
      } else {
        this.writers.push(person);
      }
    });

    console.log(this.actors);
    console.log(this.directors);
    console.log(this.writers);
  }
}
