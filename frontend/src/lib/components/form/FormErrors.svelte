<script lang="ts">
  export let data: undefined | null | { formErrors: (string | undefined | null)[], fieldErrors: object };
</script>

{#if data?.fieldErrors || data?.formErrors}
  <div>
    <ul>
      {#each ([...Object.entries(data.fieldErrors), ...Object.entries(data.formErrors)]).filter(x => x) as error}
        <li class="mt-2 text-sm text-red-500">{error}</li>
      {/each}
    </ul>
  </div>
{/if}
