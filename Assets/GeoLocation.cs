using System;

public class GeoLocation
{
    public double lat { get; set; }
    public double lon { get; set; }

    public double nlat { get; set; }
    public double nlon { get; set; }

    const double latitudeDiff = 0.004060;
    const double longitudeDiff = 0.005360;

    public GeoLocation(double latitude, double longitude)
    {
        lat = latitude;
        lon = longitude;
    }

    public GeoLocation(double latitude, double longitude, double nowlatitude, double nowlongitude)
    {
        lat = latitude;
        lon = longitude;
        nlat = nowlatitude;
        nlon = nowlongitude;
    }

}
