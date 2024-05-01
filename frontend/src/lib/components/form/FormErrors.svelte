<script lang="ts">
  export let data: undefined | null |
    {
      formErrors?: (string | null | undefined)[],
      fieldErrors?: object,
      serverErrors?: (string | null | undefined)[],
    };
</script>

<div>
  <ul>
    {#each ([...Object.entries(data?.fieldErrors ?? []), ...Object.entries(data?.formErrors ?? []), ...Object.entries(data?.serverErrors ?? [])]).filter(x => x) as error}
      <li class="mt-2 text-sm text-red-500">{error}</li>
    {/each}
  </ul>
</div>
