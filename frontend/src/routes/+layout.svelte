<script lang="ts">
  import '../app.css';
  import { AppRoute } from '$lib/routes';

  interface Item {
    text: string,
    link: string,
  }

  const items: Item[] = [
    {
      text: 'Home',
      link: AppRoute.HOME,
    },
    {
      text: 'Problems',
      link: AppRoute.PROBLEMS,
    },
    {
      text: 'Attempts',
      link: AppRoute.ATTEMPTS,
    },
  ];
</script>

<div class="flex h-screen">
  <!-- Sidebar -->
  <div class="bg-gray-800 text-white w-64 flex-shrink-0">
    <div class="flex items-center justify-center h-16 bg-gray-900">
      <h1 class="text-xl font-semibold">LC Tracker</h1>
    </div>
    <nav class="p-4">
      <ul>
        {#each items as item}
          <li class="mb-2">
            <a href="{item.link}"
               class="block py-2 px-4 bg-gray-700 hover:bg-gray-600 transition duration-300 ease-in-out rounded-lg">{item.text}</a>
          </li>
        {/each}
      </ul>
    </nav>
  </div>

  <!-- Content -->
  <div class="flex-1 overflow-hidden">
    <div class="px-6 py-4">
      <slot />
    </div>
  </div>
</div>
