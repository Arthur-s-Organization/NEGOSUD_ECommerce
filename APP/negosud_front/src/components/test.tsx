import React, { useEffect, useState } from 'react';

function Test() {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch('https://localhost:7246/api/AlcoholFamily')  // Remplace par l'URL de ton API
      .then(response => response.json())
      .then(data => setData(data));
  }, []);

  return (
    <div>
      <h1>Data from API:</h1>
      <pre>{JSON.stringify(data, null, 2)}</pre>
    </div>
  );
}

export default Test;