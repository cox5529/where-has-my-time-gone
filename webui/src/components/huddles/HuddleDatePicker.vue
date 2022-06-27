<script setup lang="ts">
import IconButton from '../IconButton.vue';
import { ref } from 'vue';

import moment from 'moment';

const props = defineProps<{ initialValue?: string }>();
const emits = defineEmits<{ (e: 'dateChange', value: string): void }>();

const value = ref(props.initialValue ?? moment().startOf('day').toISOString());

function left() {
  const newValue = moment(value.value).subtract(1, 'day');
  value.value = newValue.toISOString();
  emits('dateChange', newValue.toISOString());
}

function right() {
  const newValue = moment(value.value).add(1, 'day');
  value.value = newValue.toISOString();
  emits('dateChange', newValue.toISOString());
}

function formatDate(date: string): string {
  return moment(date).startOf('day').format('LL');
}
</script>

<template>
  <div class="flex justify-center items-center gap-8 pb-8">
    <IconButton
      type="button"
      icon="fa-solid fa-arrow-left"
      color="primary"
      @click="left"
    />
    <h4>{{ formatDate(value) }}</h4>
    <IconButton
      type="button"
      icon="fa-solid fa-arrow-right"
      color="primary"
      @click="right"
    />
  </div>
</template>
