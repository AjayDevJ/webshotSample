const webshot = require('webshot');

// Selector screenshot , added renderDelay to load the chart fully and then screenshot!!!
const optionsSelector = {
  captureSelector: '#chart',
  renderDelay: 10000
};
// This will work only if a server is running in 8080
webshot('http://localhost:8080/', 'pentacode-selector.png', optionsSelector, function(err) {
  if (!err) {
    console.log('Screenshot taken!');
  }
});