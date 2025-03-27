
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

    [JsonPropertyName("playId")]
    public string PlayId 
    { 
        get => _playId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("PlayId cannot be null or empty.", nameof(PlayId));
            _playId = value;
        }
    }

    [JsonPropertyName("audience")]
    public int Audience 
    { 
        get => _audience;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_audience), "Audience cannot be negative.");
            _audience = value;
        }
    }

    public Performance(string playId, int audience)
    {
        if (string.IsNullOrWhiteSpace(playId))
            throw new ArgumentException("PlayId cannot be null or empty.", nameof(playId));
        
        if (audience < 0)
            throw new ArgumentOutOfRangeException(nameof(_audience), "Audience cannot be negative.");

        _playId = playId;
        _audience = audience;
    }

    // Parameterless constructor for deserialization
    public Performance()
    {
        _playId = string.Empty;
        _audience = 0;
    }
}
