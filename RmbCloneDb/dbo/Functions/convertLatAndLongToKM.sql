CREATE FUNCTION [dbo].[convertLatAndLongToKM](@lat1 decimal(10,7), @long1 decimal(10,7), @lat2 decimal(10,7), @long2 decimal(10,7))
returns float as
begin
	declare @distance float;
	declare @R int = 6371;
	declare @dLat float = (@lat2-@lat1) * sin(pi() / 180.0);
	declare @dLon float = (@long2-@long1) * sin(pi() / 180.0);
	declare @a float = sin(@dLat/2.0) * sin(@dLat/2.0) +
					   cos(@lat1 * (pi() / 180.0)) * cos(@lat2 * (pi() / 180.0))*
					   sin(@dLon / 2.0) * sin(@dLon / 2.0);
	declare @c float= 2.0 * atn2(sqrt(@a), sqrt(1.0 - @a));
	set @distance = @R * @c;
	return @distance;
end;
