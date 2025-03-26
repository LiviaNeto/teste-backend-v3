using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Play
{
    private string _name;
    private int _lines;
    private PlayType _type;

    public string Name { get => _name; 
                         set
                         {
                             if (string.IsNullOrWhiteSpace(value))
                                 throw new ArgumentException("Name cannot be null or empty.", nameof(Name));

                             _name = value;
                         } 
                        }

    public int Lines { get => _lines; 
                       set
                          {
                              if (value < 0)
                                  throw new ArgumentOutOfRangeException(nameof(Lines), "Lines cannot be negative.");
                              
                              _lines = value;
                          }    
                     }
    public PlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, PlayType type) {
        if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (lines < 0)
                throw new ArgumentOutOfRangeException(nameof(lines), "Lines cannot be negative.");

        this._name = name;
        this._lines = lines;
        this._type = type;
    }
}
