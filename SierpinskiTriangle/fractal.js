class Point {
  constructor(x, y) {
    this.X = x;
    this.Y = y;
  }
}

var canvas = document.getElementById("fractalCanvas");
var ctx = canvas.getContext("2d");

var width = canvas.width;
var height = canvas.height;

var tenthOfTheHeight = Math.floor(height / 10);
var tenthOfTheWidth = Math.floor(width / 10);

var topPoint = new Point(Math.floor(width / 2), tenthOfTheHeight);
var leftPoint = new Point(tenthOfTheWidth, 9 * tenthOfTheHeight);
var rightPoint = new Point(9 * tenthOfTheWidth, 9 * tenthOfTheHeight);
var cornersArray = [topPoint, leftPoint, rightPoint];

cornersArray.forEach(element => {
  drawPoint(ctx, element);
});

var current = getRandomPointWithinTriangle(topPoint, leftPoint, rightPoint);
drawPoint(ctx, current);

var slider = document.getElementById("speedRange");

var drawInterval = slider.max - slider.value;

var interval = setInterval(() =>
  {
    drawPoint(ctx, current);
    current = getNextPoint(cornersArray, current);
  },
  drawInterval);

slider.oninput = function() {
  clearInterval(interval);

  drawInterval = slider.max - slider.value;

  interval = setInterval(() =>
    {
      drawPoint(ctx, current);
      current = getNextPoint(cornersArray, current);
    },
    drawInterval);
}

function drawPoint(canvasContext, point)
{
  canvasContext.fillStyle = "white";
  canvasContext.fillRect(point.X, point.Y, 2, 2); 
}

function getRandomPointWithinTriangle(top, left, right)
{
  var randomNumberOne = Math.random();
  var randomNumberTwo = Math.random();

  if (randomNumberOne + randomNumberTwo > 1)
  {
    randomNumberOne = 1 - randomNumberOne;
    randomNumberTwo = 1 - randomNumberTwo;
  }

  var randomXPosition = randomNumberOne * top.X + randomNumberTwo * left.X + (1 - randomNumberOne - randomNumberTwo) * right.X;
  var randomYPosition = randomNumberOne * top.Y + randomNumberTwo * left.Y + (1 - randomNumberOne - randomNumberTwo) * right.Y;

  return new Point(randomXPosition, randomYPosition);
}

function getRandomNumberFromRange(min, max)
{ 
  return Math.floor(Math.random() * (max - min + 1) + min)
}

function getRandomPointFromCollection(points)
{
  var index = getRandomNumberFromRange(0, points.length - 1)

  return points[index];
}

function getPointHalfWayBetween(pointOne, pointTwo)
{
  return new Point(
    pointOne.X + 0.5 * (pointTwo.X - pointOne.X),
    pointOne.Y + 0.5 * (pointTwo.Y - pointOne.Y)
  )
}

function getNextPoint(cornerPoints, currentPoint)
{
  var randomCorner = getRandomPointFromCollection(cornerPoints);

  var pointOnLineBetween = getPointHalfWayBetween(randomCorner, currentPoint);

  return pointOnLineBetween;
}
