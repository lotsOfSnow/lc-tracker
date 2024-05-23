<script context="module" lang="ts">
  import type { AppRoute } from '$lib/routes';

  export interface BreadcrumbItem {
    route?: AppRoute;
    label: string;
  }
</script>

<script lang="ts">
  export let items: BreadcrumbItem[] = [];
</script>

<nav aria-label="breadcrumb">
  <ul class="flex items-center space-x-2">
    {#each items as item, index}
      <li class="flex items-center">
        {#if item.route}
          <a href={item.route}
             class="px-3 py-1 bg-gray-300 text-gray-800 font-medium rounded-lg hover:bg-gray-400 transition duration-300">
            {item.label}
          </a>
        {:else}
          <span class="px-3 py-1 bg-gray-200 text-gray-600 font-medium rounded-lg">
            {item.label}
          </span>
        {/if}
        {#if index < items.length - 1}
          <svg class="mx-2 h-5 w-5 text-gray-800" fill="currentColor"
               viewBox="0 0 20 20">
            <path fill-rule="evenodd"
                  d="M7.707 14.707a1 1 0 010-1.414L11.586 10 7.707 6.707a1 1 0 111.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                  clip-rule="evenodd" />
          </svg>
        {/if}
      </li>
    {/each}
  </ul>
</nav>
