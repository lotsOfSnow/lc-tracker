<script lang="ts">
  import type { ServerFailData, ValidationFailData } from '$lib/utils/actionsReturnTypes';

  type ErrorType = ServerFailData & ValidationFailData | undefined | null;

  export let errorData: ErrorType;

  let errors: string[] = [];

  $: errors = getErrors(errorData);

  const getErrors = (errorData: ErrorType): string[] => {
    if (!errorData) {
      return [];
    }

    const all: string[] = [];

    if (errorData.validationError) {
      all.push(errorData.validationError);
    }

    if (errorData.serverError) {
      const detail = errorData.serverError.detail ? ` (${errorData.serverError.detail})` : '';
      all.push(errorData.serverError.title + detail);
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
