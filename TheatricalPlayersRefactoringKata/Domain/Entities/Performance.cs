namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; 
                           set
                           {
                                if (string.IsNullOrWhiteSpace(value))
                                 throw new ArgumentException("PlayId cannot be null or empty.", nameof(PlayId));

                                _playId = value;
                            } 
                        }
    public int Audience { get => _audience; 
                          set
                          {
                              if (value < 0)
                                  throw new ArgumentOutOfRangeException(nameof(Audience), "Audience cannot be negative.");
                              
                              _audience = value;
                          }
                        }

    public Performance() { }
    
    public Performance(string playID, int audience)
    {
        if (string.IsNullOrWhiteSpace(playID))
                throw new ArgumentException("PlayId cannot be null or empty.", nameof(playID));

        if (audience < 0)
                throw new ArgumentOutOfRangeException(nameof(audience), "Audience cannot be negative.");

        this._playId = playID;
        this._audience = audience;
    }

}
