<script lang="ts">

  import { AppRoute } from '$lib/routes';

  export let data;
</script>

<a href={AppRoute.PROBLEMS_NEW}
   class="inline-block bg-gray-700 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-lg transition duration-300 ease-in-out">
  Create new
</a>

{#each data.problems as problem}
  <div>{problem.number}</div>
  <div>{problem.name}</div>
  <div>{problem.url}</div>

{/each}
