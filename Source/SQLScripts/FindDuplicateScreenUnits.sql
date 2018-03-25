SELECT
    X, Y, COUNT(*)
FROM
    ScreenUnits
GROUP BY
    X, Y
HAVING 
    COUNT(*) > 1