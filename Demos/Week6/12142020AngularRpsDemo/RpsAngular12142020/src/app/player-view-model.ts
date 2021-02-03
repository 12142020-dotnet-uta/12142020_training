export class PlayerViewModel {

  playerId: string;
  fname: string;
  lname: string;
  numWins: number;
  numLosses: number;
  iformFile: any;
  jpgStringImage: string;

  constructor(fname?: string, lname?: string) {
    this.fname = fname;
    this.lname = lname;
  }
}
