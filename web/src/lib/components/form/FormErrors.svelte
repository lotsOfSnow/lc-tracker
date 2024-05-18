<script lang="ts">
  import type { ServerFailData, ValidationFailData } from '$lib/utils/actionsReturnTypes';

  type ErrorType = ServerFailData & ValidationFailData | undefined | null;

  export let data: ErrorType;

  let errors: string[] = [];

  $: errors = getErrors(data);

  const getErrors = (data: ErrorType): string[] => {
    if (!data) {
      return [];
    }

    const all: string[] = [];

    if (data.validationError) {
      all.push(data.validationError);
    }

    if (data.serverError) {
      const detail = data.serverError.detail ? ` (${data.serverError.detail})` : '';
      all.push(data.serverError.title + detail);
    }

    return all;

  };
</script>

<div>
  <ul>
    {#each errors as error}
      <li class="mt-2 text-sm text-red-500">{error}</li>
    {/each}
  </ul>
</div>
