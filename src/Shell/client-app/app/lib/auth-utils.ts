const visitedMicroservices: string[] = [];
let discoveredMicroservices: string[] = []; // For storing all distinct base URLs from menu load

export function addVisitedMicroservice(baseURL: string) {
  if (!visitedMicroservices.includes(baseURL)) {
    visitedMicroservices.push(baseURL);
  }
}

export function getVisitedMicroservices(): string[] {
  return [...visitedMicroservices];
}

export function setDiscoveredMicroservices(baseUrls: string[]) {
    // Ensure uniqueness upon setting the list derived from the menu load
    discoveredMicroservices = Array.from(new Set(baseUrls));
}

export function getDiscoveredMicroservices(): string[] {
    return [...discoveredMicroservices];
}